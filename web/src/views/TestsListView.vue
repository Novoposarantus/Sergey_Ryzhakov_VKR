<template>
    <div>
        <v-toolbar flat color="white">
            <v-toolbar-title>Список тестов</v-toolbar-title>
            <v-spacer></v-spacer>
            <v-btn color="primary" dark @click="createNewTest()">
                Добавить тест
            </v-btn>
        </v-toolbar>
        <v-data-table
            :headers="headers"
            :items="tests"
            class="elevation-1"
            hide-actions
            :pagination.sync="pagination"
        >
            <template v-slot:items="props">
                <td @click="editTest(props.item)">{{ props.item.name }}</td>
                <td class="text-xs-right">{{ props.item.description }}</td>
                <td class="text-xs-right">{{ props.item.questionsCount }}</td>
            </template>
            <template v-slot:no-data>
                <v-alert :value="true" color="error" icon="warning">
                    Тесты не найдены. Для добавления нажмите "Добавить тест".
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
export default {
    data () {
        return {
        pagination: {
            rowsPerPage: 20
        },
        headers: [
            {
            text: 'Имя',
            value: 'name'
            },
            { text: 'Описание', value: 'description' },
            { text: 'Количество вопросов', value: 'questionsCount' }
        ]
        }
    },
    computed: {
        ...mapGetters({
            tests : "testList/TESTS"
        }),
        pages () {
            if (this.pagination.rowsPerPage == null ||
                this.pagination.totalItems == null
            ) return 0

            return Math.ceil(this.pagination.totalItems / this.pagination.rowsPerPage)
        }
    },
    methods:{
        editTest(test){
            console.log(test);
        },
        createNewTest(){

        }
    }
}
</script>
