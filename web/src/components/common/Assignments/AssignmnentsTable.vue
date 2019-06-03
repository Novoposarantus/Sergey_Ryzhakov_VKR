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
        :items="sortedAssignments"
        :search="search"
      >
        <template v-slot:items="props">
          <td>{{ props.item.userName }}</td>
          <td class="text-xs">{{ props.item.testName }}</td>
          <td class="text-xs">{{ props.item.time }}</td>
          <td class="text-xs">{{ props.item.result }}</td>
          <td class="text-xs">{{ props.item.dateCreate }}</td>
        </template>
        <template v-slot:no-results>
          <v-alert :value="true" color="error" icon="warning">
            Информация не найдена.
          </v-alert>
        </template>
      </v-data-table>
    </v-card>
</template>

<script>
import {mapGetters} from 'vuex';
import moment from 'moment';
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
        }),
        sortedAssignments(){
            var format = "DD.MM.YYYYY HH:mm"
            return assignments.sort((first, second)=>{
                var dateFirst = moment(first.createDate,format);
                var dateSecond = moment(second.createDate,format);
                if(dateFirst.isAfter(dateSecond)){
                    return -1;
                }
                if(dateSecond.isAfter(dateFirst)){
                    return 1;
                }
                return 0;
            })
        }
    }
}
</script>
