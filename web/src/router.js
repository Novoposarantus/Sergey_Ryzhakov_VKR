import Vue from 'vue';
import VueRouter from 'vue-router';

import LoginView from '@/views/auth/LoginView.vue';
import RegistrationView from '@/views/auth/RegistrationView.vue';
import TestsList from '@/views/TestsListView.vue';

import {
    routeNames,
    roles
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
            next({name: routeNames.StartGallery});
            return true;
        }
        return false;
    }

    function isInRole(next, ...roles){
        let roleId = store.getters["auth/ROLE_ID"];
        if(roleId && roles.includes(roleId)){
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
                    if(!isInRole(next, roles.admin)) return;
                    await store.dispatch("testList/GET");
                    next();
                }  
            }
        ]
    });
}