<template>
    <v-form ref="form" class="form">
        <v-container>
            <v-toolbar flat color="transparent">
                <v-toolbar-title>{{title}}</v-toolbar-title>
                <v-spacer></v-spacer>
                <v-btn  @click="addQuestion()"
                        v-if="showAdd"
                        color="primary" 
                        dark>
                    Добавить вопрос
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
                v-model="test.name"
                :rules="[v => !!v || 'Поле не моет быть пустым']"
                label="Название"
                required
            ></v-text-field>
            <v-textarea
                v-model="test.description"
                label="Описание"
            ></v-textarea>
            <template v-for="(question, index) in questions">
                <question
                    v-if="question != null"
                    :key="index"
                    :question="question"
                    @remove="questionRemove(index)"
                    @questionChange="questionChange($event, index)">
                </question>
                <select-question 
                    v-else
                    :key="index"
                    :filteredQuestions="filteredQuestions"
                    @remove="questionRemove(index)"
                    @save="saveQuestion($event, index)">
                </select-question>
            </template>
            
        </v-container>
    </v-form>
</template>

<script>
import Question from "@/components/common/EditTest/EditTestQuestion.vue";
import SelectQuestion from "@/components/common/EditTest/EditTestSelectQuestion.vue";
import {mapGetters, mapActions} from 'vuex';
import {routeNames} from '@/support';

export default {
    components:{
        Question,
        SelectQuestion
    },
    data(){
        return {
            test:{},
            questions: []
        }
    },
    computed:{
        ...mapGetters({
            allQuestions : 'testEdit/QUESTIONS'
        }),
        isNew(){
            return !this.$route.params.id;
        },
        title(){
            if(this.isNew) {
                return "Создание нового теста"
            }
            return "Редактирование теста."
        },
        showSave(){
            return this.questions.filter(q=>!!q).length > 0;
        },
        showDeleteAnswer(){
            return !this.isNew;
        },

        filteredQuestions(){
            return this.allQuestions
            .filter(allQ => !this.questions
                .find(q=> !q || q.id == allQ.id))
        },
        showAdd(){
            return this.filteredQuestions.length > 0;
        }
    },
    methods:{
        ...mapActions({
            saveTest : "testEdit/SAVE",
            updateTest : "testEdit/UPDATE",
            deleteTest : "testEdit/DELETE"
        }),
        async save(){
            if (!this.$refs.form.validate() || !this.showSave) {
                return;
            }
            let test = {
                    ...this.test,
                    questions : this.questions.filter(q=>!!q).map(q=>({
                        id: q.id,
                        difficulty: q.difficulty,
                        referenceResponseSeconds: q.referenceResponseSeconds
                    }))
            }
            console.log(test);
            if(this.isNew){
                await this.saveTest(test);
            }else{
                await this.updateTest(test);
            }
            this.$router.push({name: routeNames.TestsList});
        },
        addQuestion(){
            this.questions.push(null);
        },
        saveQuestion(questionId, index){
            var question = this.allQuestions.find(q=> q.id == questionId);
            this.questions.splice(index, 1, {
                ...question,
                difficulty : 1,
                referenceResponseSeconds : 0
            });
        },
        questionRemove(index){
            this.questions.splice(index, 1);
        },
        questionChange({id, difficulty, referenceResponseSeconds}, index){
            var question = this.allQuestions.find(question=> question.id == id);
            this.questions.splice(index, 1, 
            {
                ...question,
                difficulty: difficulty,
                referenceResponseSeconds: referenceResponseSeconds
            });
        },
        async remove(){
            if(this.isNew) return;
            await this.deleteTest(this.test.id);
            this.$router.push({name: routeNames.TestsList});
        }
    },
    beforeMount(){
        this.test = {
            ...this.$store.getters["testEdit/TEST"]
        };
        this.questions = [
            ...this.test.questions
        ]
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
