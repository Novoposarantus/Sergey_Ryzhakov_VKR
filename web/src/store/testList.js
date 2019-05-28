import {
    request,
    defaultVuex
} from '../support';

export const testList = {
    namespaced: true,
    state:{
        ...defaultVuex.state,
        tests : null,
    },
    getters:{
        ...defaultVuex.getters,
        "TESTS"    : (state) => state.tests
    },
    mutations:{
        ...defaultVuex.mutations,
        "SET": (state, tests) => {
            state.tests = [
                ...tests
            ];
        },
        "PUSH": (state, test) => {
            state.tests.push(test);
        },
        "REMOVE": (state, test) =>{
            let index = state.tests.indexOf(test);
            state.tests.splice(index, 1);
        },
        "REMOVE_BY_ID": (state, testId) => {
            var test = state.tests.find(t=> t.Id == testId);
            let index = state.tests.indexOf(test);
            state.tests.splice(index, 1);
        },
        
        "CLEAR": (state) => {
            defaultVuex.clear(state);
            state.tests = [];
        }
    },
    actions:{
        ...defaultVuex.actions,
        "GET" : async ({commit})=>{
            commit(defaultVuex.mutations.startLoading);
            try {
                const {json} = await request(process.env.VUE_CREATE_TESTS, 'GET');
                commit("SET", json);
                commit(defaultVuex.mutations.finishLoading);
            }
            catch (error) {
                if(!error.response || error.response.status !== 400){
                    commit("AUTH_ERROR");
                    commit(defaultVuex.mutationsNames.setError);
                }
                else
                {
                    commit("AUTH_ERROR", error.response.data);
                    commit(defaultVuex.mutationsNames.setError, error.response.data);
                }
                commit(defaultVuex.mutations.finishLoading);
            }
        }
    }
};
