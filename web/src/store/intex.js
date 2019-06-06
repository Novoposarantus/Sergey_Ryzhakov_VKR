import Vue from 'vue';
import Vuex from 'vuex';
import {auth} from './auth';
import {testList} from './testList';
import {toolbar} from './toolbar';
import {questionsList} from './questionsList';
import {questionEdit} from './questionEdit';
import {testEdit} from './testEdit';
import {assignments} from './assignments'
import {userAssignments} from './userAssignments';
import {userTesting} from './userTesting';

import {
    defaultVuex
} from '@/support';

Vue.use(Vuex);

const modules = {
    auth,
    testList,
    toolbar,
    questionsList,
    questionEdit,
    testEdit,
    assignments,
    userAssignments,
    userTesting
}

export function createStore() {
    return new Vuex.Store({
        modules,
        actions:{
            "CLEAR_STORE" : ({commit}) => {
                for(let moduleName in modules){
                    commit(`${moduleName}/${defaultVuex.mutationsNames.clear}`);
                }
            }
        }
    });
}