<template>
    <v-card class="question">
        <v-layout row class="select-question">
            <v-flex xs4>
                <v-select
                    v-model="typeId"
                    :items="typesSelect"
                    single-line
                    label="Тип"
                ></v-select>
            </v-flex>
            <v-flex xs4 class="choose-question">
                <v-select
                    v-model="questionId"
                    :items="questionsSelect"
                    label="Вопрос"
                    single-line
                ></v-select>
            </v-flex>
        </v-layout>
        <v-card-actions>
            <v-btn  v-if="questionId"
                    @click="save()"
                    color="success" 
                    dark>
                Выбрать
            </v-btn>
            <v-btn  @click="remove()"
                    color="error" 
                    dark>
                Удалить
            </v-btn>
        </v-card-actions>
    </v-card>
</template>

<script>
export default {
    props:{
        filteredQuestions : Array
    },
    data(){
        return{
            typeId : null,
            questionId : null
        }
    },
    computed:{
        typesSelect(){
            return  this.filteredQuestions.map(q=>({
                value: q.questionType.id,
                text: q.questionType.name
            }));
        },
        questionsSelect(){
            return this.filteredQuestions
                .filter(question => question.questionTypeId == this.typeId)
                .map(question => ({
                    value: question.id,
                    text: question.name
                }));
        },
    },
    methods: {
        remove(){
            this.$emit("remove");
        },
        save(){
            if(!this.questionId){
                return;
            }
            this.$emit("save", this.questionId);
        }
    }
}
</script>

<style scoped>
.question{
    padding-bottom: 10px;
}
.select-question{
    margin: 0 10px;
}
.choose-question{
    margin-left: 20px;
}
</style>
