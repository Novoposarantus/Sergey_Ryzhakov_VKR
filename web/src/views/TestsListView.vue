<template>
    <div class="form">
        <v-toolbar flat color="transparent">
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
                <td class="text-xs">{{ props.item.description }}</td>
                <td class="text-xs">{{ props.item.questionsCount }}</td>
                <td class="text-xs">{{ props.item.maxResult }}</td>
                <td class="text-xs">{{ props.item.minResult }}</td>
                <td class="text-xs">{{ props.item.referenceTime }}</td>
            </template>
            <template v-slot:no-data>
                <v-alert :value="true" color="error" icon="warning">
                    Тесты не найдены. Для добавления нажмите "Добавить тест".
                </v-alert>
            </template>
        </v-data-table>
        <div class="text-xs-center pt-2" v-if="pages > 1">
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
            {
            text: 'Имя',
            value: 'name'
            },
            { text: 'Описание', value: 'description' },
            { text: 'Количество вопросов', value: 'questionsCount' },
            { text: 'Максимум', value: 'maxResult' },
            { text: 'Минимум', value: 'minResult' },
            { text: 'Оптимальное время', value: 'referenceTime' }
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
            this.$router.push({name: routeNames.TestsEditId, params: { id : test.id }})
        },
        createNewTest(){
            this.$router.push({name: routeNames.TestsEdit});
        }
    }
}
</script>

<style scoped>

.form{
    padding: 2rem;
}
</style>