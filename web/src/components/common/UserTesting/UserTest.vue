<template>
    <v-card class="card">
        <v-card-text primary-title>
                <h3 class="headline mb-0">{{question.text}}</h3>
                <answer
                    v-for="answer in question.answers"
                    :key="answer.id"
                    :answer="answer"
                    @change="questionAnswerChange(answer.id)"/>
        </v-card-text>
    </v-card>
</template>

<script>
import Answer from '@/components/common/UserTesting/UserAnswer.vue';
export default {
    components:{
        Answer
    },
    props:{
        question: Object
    },
    methods:{
        questionAnswerChange(answerId){
            this.$emit("change", answerId);
        }
    }
}
</script>

<style scoped>
.card{
    padding: 1.5rem;
}
.time-wrapper{
    text-align: end;
}
</style>
