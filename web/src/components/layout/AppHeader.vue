<template>
  <v-toolbar app>
    <v-toolbar-side-icon v-if="isAuthenticated" @click.stop="changeDrawer()"></v-toolbar-side-icon>
    <v-toolbar-title>ВКР</v-toolbar-title>
    <v-spacer></v-spacer>
    <v-toolbar-items>
        <v-btn
          flat
          :to="{ name : routeNames.Login }"
          v-if="!isAuthenticated"
        >
          <v-icon>fas fa-sign-in-alt</v-icon>
            <span class="hidden-sm-and-down title">Войти</span>
        </v-btn>
        <v-btn
          flat
          :to="{ name : routeNames.Registration }"
          v-if="!isAuthenticated"
        >
          <v-icon>fas fa-user-plus</v-icon>
            <span class="hidden-sm-and-down title">Зарегистрироваться</span>
        </v-btn>
        <v-btn
          flat
          @click="onLogout"
          v-if="isAuthenticated"
        >
          <v-icon>fas fa-sign-out-alt</v-icon>
          <span class="hidden-sm-and-down title">Выйти</span>
        </v-btn>
    </v-toolbar-items>
    <v-toolbar-title
      class="hidden-sm-and-down"
      v-if="isAuthenticated"
    >
      <v-icon class="user-name-icon">fas fa-user</v-icon>
      <span>{{userName}}</span>
    </v-toolbar-title>
  </v-toolbar>
</template>

<script>
import {mapGetters, mapActions} from 'vuex';
import {
  routeNames
} from '@/support';
export default {
  data(){
    return {
      routeNames,
    }
  },
  computed:{
    ...mapGetters({
      isAuthenticated : "auth/IS_AUTHENTICATED",
      userName : "auth/USER_NAME"
    })
  },
  methods:{
    ...mapActions({
      logout : "auth/LOGOUT",
      changeDrawer : "toolbar/CHANGE_DRAWER"
    }),
    onLogout(){
      this.logout();
      this.$router.push({name: routeNames.Login});
    }
  }
}
</script>

<style scoped>
.d-flex{
  display: flex !important;
}
.title{
  margin-left: 10px;
}
.user-name-icon{
  margin-right: 3px;
}
</style>
