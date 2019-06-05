<template>
    <v-card>
      <v-card-title>
        <v-spacer></v-spacer>
        <v-text-field
          v-model="search"
          append-icon="search"
          label="Search"
          single-line
          hide-details
        ></v-text-field>
      </v-card-title>
      <v-data-table
        hide-actions
        :headers="headers"
        :items="assignments"
        :search="search"
      >
        <template v-slot:items="props">
          <td>{{ props.item.userName }}</td>
          <td class="text-xs">{{ props.item.testName }}</td>
          <td class="text-xs">{{ props.item.time }}</td>
          <td class="text-xs">{{ props.item.result }}</td>
          <td class="text-xs">{{ props.item.dateCreate }}</td>
        </template>
        <template v-slot:no-data>
            <v-alert :value="true" color="error" icon="warning">
                Назначения не найдены.
            </v-alert>
        </template>
      </v-data-table>
    </v-card>
</template>

<script>
import {mapGetters} from 'vuex';
export default {
    data(){
        return{
            search: "",
            headers:[
                {text: "Пользователь", value: "userName", sortable: false},
                {text: "Тест", value: "testName", sortable: false},
                {text: "Время прохождения", value: "time", sortable: false},
                {text: "Оценка", value: "result", sortable: false},
                {text: "Дата назначения", value: "dateCreate", sortable: false},
            ]
        }
    },
    computed: {
        ...mapGetters({
            assignments: "assignments/ASSIGNMENTS"
        })
    }
}
</script>
