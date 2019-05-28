import {
    request,
    defaultVuex,
    url
} from '../support';

export const questionsList = {
    namespaced: true,
    state:{
        ...defaultVuex.state,
        questions : [],
    },
    getters:{
        ...defaultVuex.getters,
        "QUESTIONS"    : (state) => state.questions
    },
    mutations:{
        ...defaultVuex.mutations,
        "SET": (state, questions) => {
            state.questions = [
                ...questions
            ];
        },
        "PUSH": (state, question) => {
            state.questions.push(question);
        },
        "REMOVE": (state, question) =>{
            let index = state.tests.indexOf(question);
            state.questions.splice(index, 1);
        },
        "REMOVE_BY_ID": (state, questionId) => {
            var question = state.tests.find(t=> t.Id == questionId);
            let index = state.questions.indexOf(question);
            state.questions.splice(index, 1);
        },
        "CLEAR": (state) => {
            defaultVuex.clear(state);
            state.questions = [];
        }
    },
    actions:{
        ...defaultVuex.actions,
        "GET" : async ({commit})=>{
            commit(defaultVuex.mutationsNames.startLoading);
            try {
                const {json} = await request(url.questions, 'GET');
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
