<template>
    <v-card
        class="expansion-panel"> 
        <v-card-text>
            <v-card-tile>
                <v-layout row class="panel">
                    <v-flex xs4>
                        <v-select
                            v-model="testId"
                            :items="testsSelect"
                            single-line
                            label="Тест"
                        ></v-select>
                    </v-flex>
                    <v-flex xs4 class="panel">
                        <v-select
                            v-model="userId"
                            :items="usersSelect"
                            label="Пользователь"
                            single-line
                        ></v-select>
                    </v-flex>
                    <v-flex xs4 class="panel actions">
                        <v-btn  @click="close()"
                                color="error" 
                                dark>
                            Закрыть
                        </v-btn>
                        <v-btn  v-if="showSave"
                                @click="save()"
                                color="success" 
                                dark>
                            Сохранить
                        </v-btn>
                    </v-flex>
                </v-layout>
            </v-card-tile>
            <v-expansion-panel
                flex
                v-if="selectedTest">
                <v-expansion-panel-content flex>
                    <template v-slot:header>
                        <div>{{selectedTest.name}}</div>
                    </template>
                    <v-card v-if="selectedTest">
                        <v-card-title primary-title>
                            <div>
                                <h3 class="headline mb-0">{{selectedTest.name}}</h3>
                                <div> {{ selectedTest.description }} </div>
                            </div>
                        </v-card-title>
                        <v-card-text>
                            <question-view
                                    v-for="question in selectedTest.questions"
                                    :key="question.id"
                                    :question="question"
                                    :showActions="false">
                                </question-view>
                        </v-card-text>
                    </v-card>
                </v-expansion-panel-content>
            </v-expansion-panel>
        </v-card-text>
    </v-card>
</template>

<script>
import QuestionView from '@/components/common/EditTest/EditTestQuestion.vue';
import { mapGetters, mapActions } from 'vuex';
export default {
    components:{
        QuestionView
    },
    data(){
        return{
            testId : null,
            userId : null
        }
    },
    computed:{
        ...mapGetters({
            tests: "assignments/TESTS",
            users: "assignments/USERS"
        }),
        testsSelect(){
            return this.tests
                .slice()
                .sort((first, second)=>{
                    var nameFirst = first.name.toLowerCase();
                    var nameSecond = second.name.toLowerCase();
                    if (nameFirst < nameSecond) return -1;
                    if (nameFirst > nameSecond) return 1;
                    return 0;
                })
                .map(test =>({
                    text: test.name,
                    value: test.id
                }));
        },
        usersSelect(){
            return this.users
                .slice()
                .sort((first, second)=>{
                    var nameFirst = first.userName.toLowerCase();
                    var nameSecond = second.userName.toLowerCase();
                    if (nameFirst < nameSecond) return -1;
                    if (nameFirst > nameSecond) return 1;
                    return 0;
                })
                .map(user =>({
                    text: user.userName,
                    value: user.id
                }));
        },
        showSave(){
            return this.testId && this.userId;
        },
        selectedTest(){
            if(!this.testId) return null;
            let test = this.tests.find(t => t.id == this.testId);
            return test;
        }
    },
    methods:{
        ...mapActions({
            saveAssignment: "assignments/SAVE"
        }),
        close(){
            this.testId = null;
            this.userId = null;
            this.$emit("close");
        },
        async save(){
            if(!this.testId || !this.userId) return;
            await this.saveAssignment({
                userId: this.userId, 
                testId: this.testId
            });
            this.close();
        }
    }
}
</script>


<style scoped>
.expansion-panel{
    margin-bottom: 20px;
}
.panel{
    margin: 0 10px;
}
</style>
