import {
    request,
    defaultVuex,
    url,
    emptyQuestion
} from '../support';

export const questionEdit = {
    namespaced: true,
    state:{
        ...defaultVuex.state,
        question : null,
        questionTypes : []
    },
    getters:{
        ...defaultVuex.getters,
        "QUESTION"    : (state) => state.question,
        "TYPES" : (state) => state.questionTypes

    },
    mutations:{
        ...defaultVuex.mutations,
        "SET_QUESTION": (state, question) => {
            state.question = {
                ...question
            };
        },
        "SET_QUESTION_TYPES": (state, questionTypes) => {
            state.questionTypes = [
                ...questionTypes
            ];
        },
        "ADD_QUESTION_TYPE": (state, questionType) =>{
            var oldQT = state.questionTypes.find(qT => qT.id == questionType.id);
            if(oldQT){
                var index = state.questionTypes.indexOf(oldQT);
                state.questionTypes(index, 1, questionType);
                return;
            }
            state.questionTypes.push(questionType);
        },
        "CLEAR": (state) => {
            defaultVuex.clear(state);
            state.question = null;
            state.questionTypes = [];
        }
    },
    actions:{
        ...defaultVuex.actions,
        "GET" : async ({commit}, questionId)=>{
            commit(defaultVuex.mutationsNames.startLoading);
            //try {
                let questions = await request(url.questions + `/${questionId}`, 'GET');
                commit("SET_QUESTION", questions.json);
                let questionsTypes = await request(url.questionsTypes, 'GET');
                commit("SET_QUESTION_TYPES", questionsTypes.json);
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
                let {json} = await request(url.questionsTypes, 'GET');
                commit("SET_QUESTION_TYPES", json);
                commit("SET_QUESTION", {...emptyQuestion});
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
        "SAVE" : async ({commit}, question) => {
            commit(defaultVuex.mutationsNames.startLoading);
            //try {
                await request(url.questions, 'POST', question);
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
        "UPDATE" : async ({commit}, question) => {
            commit(defaultVuex.mutationsNames.startLoading);
            //try {
                await request(url.questions, 'PUT', question);
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
        "SAVE_TYPE" : async ({commit}, typeName) => {
            commit(defaultVuex.mutationsNames.startLoading);
            //try {
                let {json} = await request(url.questionsTypes, 'POST', {name: typeName});
                commit("ADD_QUESTION_TYPE", json);
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

    }
};
