<template>
  <div id="app">
    <v-app id="inspire">
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

export default {
  name: 'app',
  components:{
    AppHeader,
    AppFooter
  },
  async beforeMount(){
    if(this.$store.getters["auth/IS_AUTHENTICATED"]){
      await this.$store.dispatch("auth/LOAD_USER_DATA")
    }
  }
}
</script>
