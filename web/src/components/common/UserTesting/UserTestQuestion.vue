<template>
    <v-card class="card">
        <v-card-text primary-title>
                <h3 class="headline mb-0">{{question.text}}</h3>
                <div class="code" v-if="question.code" v-html="questionCode"/> 
                <div v-if="!question.isOneAnswer">
                    <answer-check-box
                        v-for="answer in question.answers"
                        :key="answer.id"
                        :answer="answer"
                        @change="questionAnswerChange(answer.id)"/>
                </div>
                <answer-radio 
                    v-else
                    :answers="question.answers"
                    @change="questionAnswerChange($event)"/>
        </v-card-text>
    </v-card>
</template>

<script>
import AnswerCheckBox from '@/components/common/UserTesting/UserAnswerCheckBox.vue';
import AnswerRadio from '@/components/common/UserTesting/UserAnswerRadio.vue';
export default {
    components:{
        AnswerCheckBox,
        AnswerRadio
    },
    props:{
        question: Object,
        radios: null
    },
    methods:{
        questionAnswerChange(answerId){
            this.$emit("change", answerId);
        }
    },
    computed:{
        questionCode(){
            return this.question.code
                .replace(new RegExp(" ", 'g'),"&nbsp")
                .split("\n")
                .join("<br>");
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
.code{
    padding: 5px;
    max-height: 200px;
    overflow-y: auto;
    border: 1px solid rgb(238, 238, 238);
    box-shadow: 5px 5px 10px rgba(122, 122, 122, 0.5);
    margin: 10px 0;
}
</style>
