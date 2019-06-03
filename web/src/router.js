import Vue from 'vue';
import VueRouter from 'vue-router';

import LoginView from '@/views/auth/LoginView.vue';
import RegistrationView from '@/views/auth/RegistrationView.vue';
import TestsList from '@/views/TestsListView.vue';
import QuestionsListView from '@/views/QuestionsListView.vue';
import EditQuestionView from '@/views/EditQuestionView.vue';
import EditTestView from '@/views/EditTestView.vue';
import AssignmentsView from '@/views/AssignmentsView.vue';

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
                redirect: async () => {
                    if(!store.getters["auth/IS_AUTHENTICATED"]){
                        return {name: routeNames.Login};
                    }
                    await awaitAuthInfo();
                    let roleId = store.getters["auth/ROLE_ID"];
                    return {
                        name: roleId == roles.admin
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
                path: '/edit-test',
                name: routeNames.TestsEdit,
                component :  EditTestView,
                beforeEnter: async (_to, _from, next) => {
                    if(isNotAuthenticated(next)) return;
                    if(!(await isInRole(next, roles.admin))) return;
                    await store.dispatch("testEdit/GET_EMPTY");
                    next();
                }  
            },
            {
                path: '/edit-test/:id',
                name: routeNames.TestsEditId,
                component :  EditTestView,
                beforeEnter: async (to, _from, next) => {
                    if(isNotAuthenticated(next)) return;
                    if(!(await isInRole(next, roles.admin))) return;
                    await store.dispatch("testEdit/GET", to.params.id);
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
            },
            {
                path: '/edit-question',
                name: routeNames.QuestionEdit,
                component :  EditQuestionView,
                beforeEnter: async (_to, _from, next) => {
                    if(isNotAuthenticated(next)) return;
                    if(!(await isInRole(next, roles.admin))) return;
                    await store.dispatch("questionEdit/GET_EMPTY");
                    next();
                }  
            },
            {
                path: '/edit-question/:id',
                name: routeNames.QuestionEditId,
                component :  EditQuestionView,
                beforeEnter: async (to, _from, next) => {
                    if(isNotAuthenticated(next)) return;
                    if(!(await isInRole(next, roles.admin))) return;
                    await store.dispatch("questionEdit/GET", to.params.id);
                    next();
                }  
            },
            {
                path: '/assignments',
                name: routeNames.Assignments,
                component :  AssignmentsView,
                beforeEnter: async (_to, _from, next) => {
                    if(isNotAuthenticated(next)) return;
                    if(!(await isInRole(next, roles.admin))) return;
                    await store.dispatch("assignments/GET");
                    next();
                }  
            },
        ]
    });
}