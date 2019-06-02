import {
    request,
    defaultVuex,
    url,
    emptyTest
} from '../support';

export const questionEdit = {
    namespaced: true,
    state:{
        ...defaultVuex.state,
        test : null,
        questions: []
    },
    getters:{
        ...defaultVuex.getters,
        "TEST"    : (state) => state.test,
        "QUESTIONS" : (state) => state.questions
    },
    mutations:{
        ...defaultVuex.mutations,
        "SET_TEST_INFO": (state, editTest) => {
            state.test = {
                ...editTest.test
            };
            state.questions = [
                ...editTest.allQuestions
            ];
        },
        "SET_TEST": (state, test) => {
            state.test = {
                ...test
            };
        },
        "SET_QUESTIONS": (state, questions) => {
            state.questions = [
                ...questions
            ];
        },
        "CLEAR": (state) => {
            defaultVuex.clear(state);
            state.test = null;
            state.questions = [];
        }
    },
    actions:{
        ...defaultVuex.actions,
        "GET" : async ({commit}, questionId)=>{
            commit(defaultVuex.mutationsNames.startLoading);
            //try {
                let {json} = await request(url.tests + `/${questionId}`, 'GET');
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
        "GET_EMPTY" : async ({commit}) => {
            commit(defaultVuex.mutationsNames.startLoading);
            //try {
                let {json} = await request(url.testsAllQuestions, 'GET');
                commit("SET_TEST", emptyTest);
                commit("SET_QUESTIONS", json);
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
        "SAVE" : async ({commit}, test) => {
            commit(defaultVuex.mutationsNames.startLoading);
            //try {
                await request(url.tests, 'POST', test);
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
        "UPDATE" : async ({commit}, test) => {
            commit(defaultVuex.mutationsNames.startLoading);
            //try {
                await request(url.tests, 'PUT', test);
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
        "DELETE" : async ({commit}, testId) => {
            commit(defaultVuex.mutationsNames.startLoading);
            //try {
                await request(url.tests + `/${testId}`, 'DELETE');
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
