<template>
    <div>
        <v-card v-if="unsolvedAssignmentsAny" class="card">
            <v-card-title>
              <h3 class="headline mb-0">У вас есть непройденные тесты.</h3>
            </v-card-title>
            <v-card-text>
                <v-layout row wrap>
                    <v-flex xs4>
                            <v-select
                                v-model="testId"
                                :items="testsSelect"
                                single-line
                                label="Тест"
                            ></v-select>
                    </v-flex>
                    <v-flex xs4 class="button">
                        <v-btn  v-if="testId"
                                @click="start()"
                                color="success" 
                                dark>
                            Начать
                        </v-btn>
                    </v-flex>
                </v-layout>
            </v-card-text>
        </v-card>
        <a-table></a-table>
    </div>
</template>

<script>
import ATable from '@/components/common/UserTesting/UserAssignmentsList.vue';
import { mapGetters } from 'vuex';
export default {
    components:{
        ATable
    },
    data(){
        return{
            testId: null
        }
    },
    computed:{
        ...mapGetters({
            assignments: "userAssignments/ASSIGNMENTS"
        }),
        unsolvedAssignments(){
            return this.assignments.filter(assignment => assignment.result == null);
        },
        unsolvedAssignmentsAny(){
            return this.unsolvedAssignments.length > 0;
        },
        testsSelect(){
            return this.unsolvedAssignments.map(assignment => ({
                text: assignment.testName,
                value: assignment.testId
            }));
        }
    },
    methods:{
        start(){

        }
    }
}
</script>

<style scoped>
.card{
    padding: 2rem;
    margin-bottom: 20px;
}
.button{
    padding-top: 8px;
}
</style>
