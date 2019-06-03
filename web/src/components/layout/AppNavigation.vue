<template>
    <v-navigation-drawer
      v-model="drawer"
      v-if="isAuthenticated"
      fixed
      app
    >
      <v-list dense>
        <v-list-tile @click="toTests()" v-if="isAdmin">
          <v-list-tile-action>
            <v-icon>fas fa-clipboard-list</v-icon>
          </v-list-tile-action>
          <v-list-tile-content>
            <v-list-tile-title>Тесты</v-list-tile-title>
          </v-list-tile-content>
        </v-list-tile>
        <v-list-tile @click="toQuestions()" v-if="isAdmin">
          <v-list-tile-action>
            <v-icon>fas fa-question-circle</v-icon>
          </v-list-tile-action>
          <v-list-tile-content>
            <v-list-tile-title>Вопросы</v-list-tile-title>
          </v-list-tile-content>
        </v-list-tile>
        <v-list-tile @click="toAssignments()" v-if="isAdmin">
          <v-list-tile-action>
            <v-icon>fas fa-user-graduate</v-icon>
          </v-list-tile-action>
          <v-list-tile-content>
            <v-list-tile-title>Назначения</v-list-tile-title>
          </v-list-tile-content>
        </v-list-tile>
      </v-list>
    </v-navigation-drawer>
</template>

<script>
import {mapGetters, mapActions} from 'vuex';
import {routeNames, roles} from '@/support';
export default {
    computed:{
        ...mapGetters({
            vuexDraver : 'toolbar/DRAWER',
            isAuthenticated: 'auth/IS_AUTHENTICATED',
            roleId : 'auth/ROLE_ID'
        }),
        drawer: {
            get: function () {
                return this.vuexDraver;
            },
            set: function (event) {this.changeDrawer(event)}
        },
        isAdmin(){
          return this.roleId == roles.admin;
        },
        isUser(){
          return this.roleId == roles.user;
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
        },
        toAssignments(){
          this.$router.push({name: routeNames.Assignments});
        }
    },
}
</script>
