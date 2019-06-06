import {
    request,
    defaultVuex,
    url
} from '../support';

export const userAssignments = {
    namespaced: true,
    state:{
        ...defaultVuex.state,
        assignments: []
    },
    getters:{
        ...defaultVuex.getters,
        "ASSIGNMENTS" : (state) => state.assignments,
    },
    mutations:{
        ...defaultVuex.mutations,
        "SET": (state, assignments) => {
            state.assignments = [
                ...assignments
            ];
        },
        "CLEAR": (state) => {
            defaultVuex.clear(state);
            state.assignments = [];
        }
    },
    actions:{
        ...defaultVuex.actions,
        "GET" : async ({commit})=>{
            commit(defaultVuex.mutationsNames.startLoading);
            //try {
                let {json} = await request(url.assignmnents + "/GetByUser", 'GET');
                commit("SET", json);
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
