<template>
    <v-card class="question">
        <v-card-text>
            <div>
                <table class="d-inline-block table">
                    <tr class="table">
                        <td class="font-16">Сложность:</td>
                        <td><v-rating   
                                :readonly="readonly"
                                v-model="difficulty"
                                background-color="red lighten-3"
                                class="d-inline-block"
                                small
                                color="red"></v-rating></td>
                    </tr>
                </table>
                <table class="d-inline-block table time-table">
                    <tr>
                        <td class="table time-table">
                            <v-text-field 
                                v-if="!readonly"
                                class="d-inline-block m-r-20"
                                label="время решения"
                                :rules="[v => !!v || 'Поле не моет быть пустым']"
                                v-model="referenceResponseSeconds"
                                type="number"
                            ></v-text-field>
                            <span class="font-16 m-r-10" v-else>Оптимальное время решения:</span>
                        </td>
                        <td class="font-16">{{referenceResponseSecondsString}}</td>
                    </tr>
                </table>
            </div>
            <h3 class="headline mb-0">{{question.text}}</h3>
            <div class="code" v-if="question.code" v-html="questionCode"/> 
            <table class="answers">
                <tr v-for="(answer, index) in question.answers"
                    :key="answer.id">
                    <td class="indexer"><v-icon v-if="answer.isRight">fa-check-square</v-icon></td>
                    <td class="answer-box answer indexer">{{index + 1}})<td>
                    <td class="answer-box"><span class="answer">{{answer.text}}</span></td>
                </tr>
            </table>
        </v-card-text>
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
        question: Object,
        showActions:{
            type: Boolean,
            default: true
        },
        readonly:{
            type: Boolean,
            default: false
        }
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
                    difficulty: newValue,
                    referenceResponseSeconds: this.referenceResponseSeconds
                });
            }
        },
        referenceResponseSeconds:{
            get(){
                return this.question.referenceResponseSeconds;
            },
            set(newValue){
                this.$emit("questionChange", {
                    id: this.question.id, 
                    difficulty: this.difficulty,
                    referenceResponseSeconds: newValue
                });
            }
        },
        referenceResponseSecondsString(){
            if(this.referenceResponseSeconds == null) return "00:00:00";
            let allSeconds = this.referenceResponseSeconds;
            let hours = parseInt(allSeconds / 3600); 
            let hoursStr = `${hours < 10 ? "0" : ""}${hours}`
            let remainder = allSeconds % 3600;
            let minutes = parseInt(remainder / 60); 
            let minutesStr = `${minutes < 10 ? "0" : ""}${minutes}`; 
            let seconds = remainder % 60;
            let secondsStr = `${seconds < 10 ? "0" : ""}${seconds}`;
            return `${hoursStr}:${minutesStr}:${secondsStr}`;
        },
        questionCode(){
            return this.question.code
                .replace(new RegExp(" ", 'g'),"&nbsp")
                .split("\n")
                .join("<br>");
        }
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
    padding-bottom: 5px;
    margin-bottom: 15px;
}
li {
    list-style-type: none;
}
.empty-icon{
    width: 21px;
    height: 100%;
    display: inline-block;
}
.answer{
    font-size: 18px;
}
.answer-box{
    padding-left: 10px;
    padding-bottom: 5px;
}
.indexer{
  vertical-align: top;
}
.font-16{
    font-size: 16px;
}
.m-r-20{
    margin-right: 20px;
}

.m-r-10{
    margin-right: 10px;
}

@media only screen 
and (max-width : 770px) {
}
@media only screen 
and (min-width : 770px) {
    .table{
        height: 68px;
    }
    .time-table{
        margin-left: 20px;
    }
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