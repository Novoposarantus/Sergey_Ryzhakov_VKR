<template>
    <div class="wrapper">
        <div class="progress"
            v-if="showQuestions">
            <v-progress-linear v-model="progress"/> 
        </div>
        <start-page 
            :message="test.description"
            v-if="viewStartPage"
            @start="start()"/>
        <question
            v-if="showQuestions"
            :question="viewedQuestion"
            @change="answerChange($event)"/>
        <div class="actions">
            <v-btn
                v-if="showNext"
                color="primary"
                large
                dark
                @click="next()">
                Следуйщий
            </v-btn>
            <v-btn
                v-if="showEnd"
                color="success"
                large
                dark
                @click="end()">
                Завершить
            </v-btn>
        </div>
    </div>
</template>

<script>
import StartPage from '@/components/common/UserTesting/TestingStartPage.vue'
import Question from '@/components/common/UserTesting/UserTestQuestion.vue'
import { mapGetters, mapActions } from 'vuex';
import {routeNames} from '@/support';
import { setInterval, clearInterval } from 'timers';
export default {
    components:{
        StartPage,
        Question
    },
    data(){
        return{
            viewedQuestionIndex: null,
            chackedAnswersIds: [],
            times: {},
            interval: null
        }
    },
    computed:{
        ...mapGetters({
            test: "userTesting/TEST"
        }),
        viewedQuestion(){
            if(this.viewedQuestionIndex == null 
            || this.viewedQuestionIndex >= this.test.questions.length
            || this.viewedQuestionIndex < 0){
                return null;
            }
            return this.test.questions[this.viewedQuestionIndex];
        },
        showQuestions(){
            return this.viewedQuestion != null;
        },
        viewStartPage(){
            return this.viewedQuestionIndex == null;
        },
        showNext(){
            if(this.viewedQuestionIndex == null) return false;
            return this.viewedQuestionIndex < this.test.questions.length - 1;
        },
        showEnd(){
            if(this.viewedQuestionIndex == null) return false;
            return this.viewedQuestionIndex == this.test.questions.length - 1
        },
        progress(){
            if(this.viewedQuestionIndex == null){
                return 0;
            }
            return (this.viewedQuestionIndex/this.test.questions.length) * 100;
        }   
    },
    methods:{
        ...mapActions({
            saveResult: "userTesting/Save"
        }),
        answerChange(answerId){
            let index = this.chackedAnswersIds.indexOf(answerId);
            if(index < 0){
                this.chackedAnswersIds.push(answerId);
            }else{
                this.chackedAnswersIds.splice(index, 1);
            }
        },
        start(){
            this.viewedQuestionIndex = 0;
            this.interval = setInterval(() => {
                if(this.times[this.viewedQuestion.id] == null){
                    this.$set(this.times, this.viewedQuestion.id, 0);
                    this.times[this.viewedQuestion.id] = 0;
                }
                this.$set(this.times, this.viewedQuestion.id, this.times[this.viewedQuestion.id] + 1000);
            }, 1000)
        },
        next(){
            this.viewedQuestionIndex += 1;
        },
        async end(){
            clearInterval(this.interval);
            let assignmentId = this.test.assignmentId;
            let checkedAnswers = this.chackedAnswersIds;
            let times = [];
            for(var key in this.times){
                times.push({
                    id: key,
                    Milliseconds: this.times[key]
                });
            }
            await this.saveResult({
                assignmentId,
                checkedAnswers,
                times
            })
            this.$router.push({name: routeNames.TestingList});
        }
    }
}
</script>

<style scoped>
.progress{
    padding: 0 20px;
}
.wrapper{
    padding: 2rem;
}
.actions{
    margin-top: 1rem;
    text-align: end;
}
</style>
