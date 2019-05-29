<template>
    <div>
        <v-toolbar flat color="white">
            <v-toolbar-title>Список вопросов</v-toolbar-title>
            <v-spacer></v-spacer>
            <v-btn color="primary" dark @click="createNewQuestion()">
                Создать вопрос
            </v-btn>
        </v-toolbar>
        <v-data-table
            :headers="headers"
            :items="questions"
            class="elevation-1"
            hide-actions
            :pagination.sync="pagination"
        >
            <template v-slot:items="props">
                <td @click="editQuestion(props.item)">{{ props.item.text }}</td>
                <td class="text-xs">{{ props.item.typeName }}</td>
                <td class="text-xs">{{ props.item.answersCount }}</td>
                <td class="text-xs">{{ props.item.rightAnswersCount }}</td>
            </template>
            <template v-slot:no-data>
                <v-alert :value="true" color="error" icon="warning">
                    Вопросы не найдены. Для добавления нажмите "Создать вопрос".
                </v-alert>
            </template>
        </v-data-table>
        <div class="text-xs-center pt-2">
            <v-pagination v-model="pagination.page" :length="pages"></v-pagination>
        </div>
    </div>
</template>

<script>
import { mapGetters } from 'vuex';
import {routeNames} from '@/support';
export default {
    data () {
        return {
        pagination: {
            rowsPerPage: 20
        },
        headers: [
            { text: 'Вопрос', value: 'text' },
            { text: 'Тип', value: 'typeName' },
            { text: 'Количество ответов', value: 'answersCount' },
            { text: 'Количество правильных ответов', value: 'rightAnswersCount' }
        ]
        }
    },
    computed: {
        ...mapGetters({
            questions : "questionsList/QUESTIONS"
        }),
        pages () {
            if (this.pagination.rowsPerPage == null ||
                this.pagination.totalItems == null
            ) return 0

            return Math.ceil(this.pagination.totalItems / this.pagination.rowsPerPage)
        }
    },
    methods:{
        editQuestion(question){
            this.$router.push({name: routeNames.QuestionEditId, params: { id : question.id }})
        },
        createNewQuestion(){
            this.$router.push({name: routeNames.QuestionEdit})
        }
    }
}
</script>
