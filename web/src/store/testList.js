import {
    request,
    defaultVuex,
    url
} from '../support';

export const testList = {
    namespaced: true,
    state:{
        ...defaultVuex.state,
        tests : [],
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
            commit(defaultVuex.mutationsNames.startLoading);
            try {
                const {json} = await request(url.tests, 'GET');
                commit("SET", json);
                commit(defaultVuex.mutationsNames.finishLoading);
            }
            catch (error) {
                if(!error.response || error.response.status !== 400){
                    commit(defaultVuex.mutationsNames.setError);
                }
                else
                {
                    commit(defaultVuex.mutationsNames.setError, error.response.data);
                }
                commit(defaultVuex.mutationsNames.finishLoading);
            }
        }
    }
};
