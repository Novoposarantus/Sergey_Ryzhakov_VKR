import {
    request,
    defaultVuex,
    url
} from '../support';

export const assignments = {
    namespaced: true,
    state:{
        ...defaultVuex.state,
        tests : [],
        assignments: [],
        users : []
    },
    getters:{
        ...defaultVuex.getters,
        "TESTS"    : (state) => state.tests,
        "ASSIGNMENTS" : (state) => state.assignments,
        "USERS" : (state) => state.users
    },
    mutations:{
        ...defaultVuex.mutations,
        "SET": (state, {tests, assignments, users}) => {
            state.tests = [
                ...tests
            ];
            state.assignments = [
                ...assignments
            ];
            state.users = [
                ...users
            ];
        },
        "ADD_ASSIGNMNENT": (state, assignment) => {
            state.assignments.push(assignment);
        },
        "CLEAR": (state) => {
            defaultVuex.clear(state);
            state.tests = [];
            state.assignments = [];
            state.users = [];
        }
    },
    actions:{
        ...defaultVuex.actions,
        "GET" : async ({commit})=>{
            commit(defaultVuex.mutationsNames.startLoading);
            //try {
                let {json} = await request(url.assignmnents, 'GET');
                commit("SET_TEST_INFO", json);
                commit(defaultVuex.mutationsNames.finishLoading);
            // }
            // catch (error) {
            //     if(!error.response || error.response.status !== 400){
            //         commit(defaultVuex.mutationsNames.setError);
            //     }
            //     else
            //     {
            //         commit(defaultVuex.mutationsNames.setError, error.response.data);
            //     }
            //     commit(defaultVuex.mutationsNames.finishLoading);
            // }
        },
        "SAVE" : async ({commit}, assignment) => {
            commit(defaultVuex.mutationsNames.startLoading);
            //try {
                let {json} = await request(url.assignmnents, 'POST', assignment);
                commit("ADD_ASSIGNMNENT", json);
                commit(defaultVuex.mutationsNames.finishLoading);
            // }
            // catch (error) {
            //     if(!error.response || error.response.status !== 400){
            //         commit(defaultVuex.mutationsNames.setError);
            //     }
            //     else
            //     {
            //         commit(defaultVuex.mutationsNames.setError, error.response.data);
            //     }
            //     commit(defaultVuex.mutationsNames.finishLoading);
            // }
        }
    }
};
