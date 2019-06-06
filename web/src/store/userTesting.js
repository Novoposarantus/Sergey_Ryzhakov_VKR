import {
    request,
    defaultVuex,
    url
} from '../support';

export const userTesting = {
    namespaced: true,
    state:{
        ...defaultVuex.state,
        test: null
    },
    getters:{
        ...defaultVuex.getters,
        "TEST" : (state) => state.test,
    },
    mutations:{
        ...defaultVuex.mutations,
        "SET": (state, test) => {
            state.test = {
                ...test
            };
        },
        "CLEAR": (state) => {
            defaultVuex.clear(state);
            state.test = [];
        }
    },
    actions:{
        ...defaultVuex.actions,
        "GET" : async ({commit}, assignmentId)=>{
            commit(defaultVuex.mutationsNames.startLoading);
            try {
                let {json} = await request(url.testWork + `/${assignmentId}`, 'GET');
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
        },
        "Save": async ({commit}, result)=>{
            commit(defaultVuex.mutationsNames.startLoading);
            try {
                await request(url.testWork, 'POST', result);
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
        },
    }
};
