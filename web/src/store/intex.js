import Vue from 'vue';
import Vuex from 'vuex';
import {auth} from './auth';
import {testList} from './testList';
import {toolbar} from './toolbar';

import {
    defaultVuex
} from '@/support';

Vue.use(Vuex);

const modules = {
    auth,
    testList,
    toolbar
}

export function createStore() {
    return new Vuex.Store({
        modules,
        actions:{
            "CLEAR_STORE" : ({commit}) => {
                for(let moduleName in modules){
                    commit(`${moduleName}/${defaultVuex.clear}`);
                }
            }
        }
    });
}