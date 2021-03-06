<template>
    <v-form ref="form" class="form">
        <v-container>
            <v-toolbar flat color="transparent">
                <v-toolbar-title>{{title}}</v-toolbar-title>
                <v-spacer></v-spacer>
                <v-btn  v-if="showSave"
                        @click="showName()"
                        color="primary" 
                        dark>
                    Добавить имя
                </v-btn>
                <v-btn  v-if="showSave"
                        @click="showCodePlace()"
                        color="primary" 
                        dark>
                    Добавить описание
                </v-btn>
                <v-btn  v-if="showSave"
                        @click="save()"
                        color="success" 
                        dark>
                    Сохранить
                </v-btn>
                <v-btn  v-if="!isNew"
                        @click="remove()"
                        color="error" 
                        dark>
                    Удалить
                </v-btn>
            </v-toolbar>
            <v-text-field
                v-if="questionNameShow"
                v-model="question.name"
                label="Имя"
                required
            ></v-text-field>
            <v-text-field
                v-model="question.text"
                :rules="[v => !!v || 'Поле не моет быть пустым']"
                label="Вопрос"
                required
            ></v-text-field>
            <v-textarea
                v-if="codePlaceSwow"
                rows="10"
                v-model="question.code"
                label="Дополнительно">

            </v-textarea>
            <div>
                <v-select
                    :items="types"
                    v-model="question.questionTypeId"
                    label="Тип"
                    class="select-field"
                    :rules="[v => !!v || 'Выберите тип вопроса']"
                ></v-select>
                <question-type-dialog></question-type-dialog>
            </div>
            
            <v-toolbar flat color="transparent">
                <v-toolbar-title>Ответы</v-toolbar-title>
                <v-spacer></v-spacer>
                <v-btn  @click="addAnswer()"
                        color="primary" 
                        dark>
                    Добавить ответ
                </v-btn>
            </v-toolbar>
            <v-alert
                :value="answerAlert"
                type="error"
                transition="scale-transition"
            >
                Выберите хотя бы 1 правильный ответ.
            </v-alert>
            <edit-answer 
                v-for="answer in  question.answers"
                :key="answer.Id"
                :value="answer"
                :showDelete="showDeleteAnswer"
                @change="answerChange($event)"
                @delete="answerDelete($event)"    
            ></edit-answer>
        </v-container>
    </v-form>
</template>

<script>
import QuestionTypeDialog from "@/components/common/EditQuestion/QuestionTypeDialog.vue";
import EditAnswer from "@/components/common/EditQuestion/EditAnswer.vue";
import {mapGetters, mapActions} from 'vuex';
import {emptyAnswer,routeNames, minAnswersCount} from '@/support';

export default {
    components:{
        QuestionTypeDialog,
        EditAnswer
    },
    data(){
        return {
            question:{},
            answerAlert: false,
            codePlaceSwow: false,
            questionNameShow: false
        }
    },
    computed:{
        ...mapGetters({
            vuexTypes: "questionEdit/TYPES"
        }),
        isNew(){
            return !this.$route.params.id;
        },
        title(){
            if(this.isNew) {
                return "Создание нового вопроса"
            }
            return "Редактирование вопроса."
        },
        showSave(){
            return this.question.answers.length >= minAnswersCount;
        },
        showDeleteAnswer(){
            return this.question.answers.length > minAnswersCount;
        },
        types(){
            return this.vuexTypes.map(el=>({
                text: el.name,
                value: el.id
            }));
        },
        rightAnswersCount(){
            return this.question.answers.filter(x=>x.isRight).length;
        }
    },
    methods:{
        ...mapActions({
            saveQuestion : "questionEdit/SAVE",
            updateQuestion : "questionEdit/UPDATE",
            deleteQuestion : "questionEdit/DELETE"
        }),
        addAnswer(){
            this.question.answers.push({
                ...emptyAnswer
            });
        },
        answerChange({oldValue, newValue}){
            this.answerAlert = false;
            let index = this.question.answers.indexOf(oldValue);
            this.question.answers.splice(index, 1, {...newValue});
        },
        answerDelete(answer){
            let index = this.question.answers.indexOf(answer);
            this.question.answers.splice(index, 1);
        },
        showCodePlace(){
            this.codePlaceSwow = !this.codePlaceSwow;
            if(!this.codePlaceSwow){
                this.question.code = null;
            }
        },
        showName(){
            this.questionNameShow = !this.questionNameShow;
            if(!this.questionNameShow){
                this.question.name = null;
            }
        },
        async save(){
            if (!this.$refs.form.validate() || this.rightAnswersCount == 0) {
                this.answerAlert = this.rightAnswersCount == 0;
                return;
            }
            if(this.isNew){
                await this.saveQuestion({...this.question});
            }else{
                await this.updateQuestion({...this.question});
            }
            this.$router.push({name: routeNames.QuestionsList});
        },
        async remove(){
            if(this.isNew) return;
            await this.deleteQuestion(this.question.id);
            this.$router.push({name: routeNames.QuestionsList});
        }
    },
    beforeMount(){
        var question = this.$store.getters["questionEdit/QUESTION"];
        this.question = {
            ...question,
            answers:[
                ...question.answers.map(a=> ({...a}))
            ]
        },
        this.questionNameShow = !!question.name;
        this.codePlaceSwow = !!question.code;
    }
}
</script>

<style scoped>
.select-field{
    max-width: 250px;
    display: inline-block;
}
.d-flex{
    display: flex;
}
.form{
    padding: 2rem;
}
</style>
