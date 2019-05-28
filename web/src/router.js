import Vue from 'vue';
import VueRouter from 'vue-router';

import LoginView from '@/views/auth/LoginView.vue';
import RegistrationView from '@/views/auth/RegistrationView.vue';
import TestsList from '@/views/TestsListView.vue';
import QuestionsListView from '@/views/QuestionsListView.vue';

import {
    routeNames,
    roles,
    defaultVuex
} from './support';

Vue.use(VueRouter);

export function createRouter (store) {
    function isNotAuthenticated(next){
        if (!store.getters["auth/IS_AUTHENTICATED"]) {
            next({name: routeNames.Login});
            return true;
        }
    
        return false;
    }
 
    function isAuthenticated(next){
        if (store.getters["auth/IS_AUTHENTICATED"]) {
            next({name: routeNames.TestsList});
            return true;
        }
        return false;
    }

    async function awaitAuthInfo(){
        return await new Promise(async (resolve, reject) =>
        {
            setTimeout(function tick(){
                if(store.getters["auth/IS_AUTHENTICATED"] 
                && store.getters["auth/AUTH_INFO_LOADED"]){
                    resolve();
                    return;
                }
                if(!store.getters["auth/IS_AUTHENTICATED"]){
                    resolve();
                    return;
                }
                if(store.getters["auth/IS_AUTHENTICATED"] && store.getters[defaultVuex.globalGettersNames("auth").error] ){
                    reject();
                    return;
                }
                setTimeout(tick, 1000);
            },1000)
        })
    }

    async function isInRole(next, ...roles){
        await awaitAuthInfo();
        let roleId = store.getters["auth/ROLE_ID"];
        if(roleId != null && roles.includes(roleId)){
            return true;
        }
        next({name: routeNames.Login});
        return false;
    }

    return new VueRouter({
        mode: 'history',
        routes : [
            {
                path: '/',
                name : routeNames.Start,
                redirect: () => {
                    return {
                        name: store.getters["auth/IS_AUTHENTICATED"]
                        ? routeNames.TestsList
                        : routeNames.Login
                    }
                }
            },
            {
                path: '/login',
                name: routeNames.Login,
                component :  LoginView,
                beforeEnter: (_to, _from, next) => {
                    if(isAuthenticated(next)) return;
                    next();
                }
            },
            {
                path: '/registration',
                name: routeNames.Registration,
                component :  RegistrationView,
                beforeEnter: (_to, _from, next) => {
                    if(isAuthenticated(next)) return;
                    next();
                }
            },
            {
                path: '/tests-list',
                name: routeNames.TestsList,
                component :  TestsList,
                beforeEnter: async (_to, _from, next) => {
                    if(isNotAuthenticated(next)) return;
                    if(!(await isInRole(next, roles.admin))) return;
                    await store.dispatch("testList/GET");
                    next();
                }  
            },
            {
                path: '/questions-list',
                name: routeNames.QuestionsList,
                component :  QuestionsListView,
                beforeEnter: async (_to, _from, next) => {
                    if(isNotAuthenticated(next)) return;
                    if(!(await isInRole(next, roles.admin))) return;
                    await store.dispatch("questionsList/GET");
                    next();
                }  
            }
        ]
    });
}