<template>
    <v-navigation-drawer
      v-model="drawer"
      fixed
      app
    >
      <v-list dense>
        <v-list-tile @click="toTests()">
          <v-list-tile-action>
            <v-icon>fas fa-clipboard-list</v-icon>
          </v-list-tile-action>
          <v-list-tile-content>
            <v-list-tile-title>Тесты</v-list-tile-title>
          </v-list-tile-content>
        </v-list-tile>
        <v-list-tile @click="toQuestions()">
          <v-list-tile-action>
            <v-icon>fas fa-question-circle</v-icon>
          </v-list-tile-action>
          <v-list-tile-content>
            <v-list-tile-title>Вопросы</v-list-tile-title>
          </v-list-tile-content>
        </v-list-tile>
      </v-list>
    </v-navigation-drawer>
</template>

<script>
import {mapGetters, mapActions} from 'vuex';
import {routeNames} from '@/support';
export default {
    computed:{
        ...mapGetters({
            vuexDraver : 'toolbar/DRAWER'
        }),
        drawer: {
            get: function () {
                return this.vuexDraver;
            },
            set: function (event) {this.changeDrawer(event)}
        }
    },
    methods: {
        ...mapActions({
            changeDrawer : "toolbar/CHANGE_DRAWER"
        }),
        toTests(){
          this.$router.push({name: routeNames.TestsList});
        },
        toQuestions(){
          this.$router.push({name: routeNames.QuestionsList});
        }
    },
}
</script>
