<template>
    <v-form ref="form" class="form">
        <v-container>
            <v-toolbar flat color="transparent">
                <v-toolbar-title>Назначения</v-toolbar-title>
                <v-spacer></v-spacer>
                <v-btn  @click="addAssignments()"
                        v-if="showAdd"
                        color="primary" 
                        dark>
                    Добавить назначение
                </v-btn>
            </v-toolbar>
            <assignments-table></assignments-table>
        </v-container>
    </v-form>
</template>

<script>
import AssignmentsTable from "@/components/common/Assignments/AssignmentsTable.vue";
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
                        difficulty: q.difficulty
                    }))
            }
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
                difficulty : 1
            });
        },
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
