<template>
    <v-card class="question">
        <v-card-title primary-title>
            <div>
                <div>
                    <h3 class="headline mb-0 d-inline-block difficulty">Сложность:</h3>
                    <v-rating   v-model="difficulty"
                                background-color="red lighten-3"
                                class="d-inline-block"
                                small
                                color="red"></v-rating>
                </div>
                <h3 class="headline mb-0">{{question.text}}</h3>
                <ul>
                    <li v-for="answer in question.answers"
                        :key="answer.id">
                        <v-icon v-if="answer.isRight">fa-check-square</v-icon>
                        <span v-else class="empty-icon"></span>
                        <span class="answer">{{answer.text}}</span>
                    </li>
                </ul>
            </div>
        </v-card-title>
        <v-card-actions>
            <v-btn @click="remove()"
                    color="error" 
                    dark>
                Удалить
            </v-btn>
        </v-card-actions>
    </v-card>
</template>

<script>
import { mapGetters } from 'vuex';
export default {
    props:{
        question: Object
    },
    data(){
        return{
            typeId: this.question.questionTypeId
        }
    },
    computed:{
        ...mapGetters({
            questions : 'testEdit/QUESTIONS'
        }),
        difficulty:{
            get(){
                return this.question.difficulty;
            },
            set(newValue){
                this.$emit("questionChange", {
                    id: this.question.id, 
                    difficulty: newValue
                });
            }
        },
    },
    methods: {
        remove(){
            this.$emit("remove");
        }
    }
}
</script>

<style scoped>
.question{
    padding-bottom: 10px;
}
li {
    list-style-type: none;
}
.empty-icon{
    width: 21px;
    display: inline-block;
}
.answer{
    font-size: 20px;
    padding: 2px 0;
    margin-left: 5px;
}
</style>