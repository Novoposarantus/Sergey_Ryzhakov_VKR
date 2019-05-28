<template>
  <div id="app">
    <v-app id="inspire">
      <app-navigation v-if="isAuthenticated"></app-navigation>
      <app-header></app-header>
      <v-content>
          <router-view></router-view>
      </v-content>
      <app-footer></app-footer>
    </v-app>
  </div>
</template>

<script>
import AppHeader from './components/layout/AppHeader';
import AppFooter from './components/layout/AppFooter';
import AppNavigation from './components/layout/AppNavigation';
import { mapGetters } from 'vuex';

export default {
  name: 'app',
  components:{
    AppHeader,
    AppFooter,
    AppNavigation
  },
  computed:{
    ...mapGetters({
      isAuthenticated: 'auth/IS_AUTHENTICATED',
    })
  },
  async beforeMount(){
    if(this.$store.getters["auth/IS_AUTHENTICATED"]){
      await this.$store.dispatch("auth/LOAD_USER_DATA")
    }
  }
}
</script>
