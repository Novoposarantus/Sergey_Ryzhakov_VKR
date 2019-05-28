import Cookie from 'js-cookie';
import {
    request,
    defaultVuex
} from '../support';

export const auth = {
    namespaced: true,
    state:{
        ...defaultVuex.state,
        isAuthenticated : !!Cookie.get('user-token'),
        userData        : null
    },
    getters:{
        ...defaultVuex.getters,
        "IS_AUTHENTICATED"    : (state) => state.isAuthenticated,
        "USER_NAME"           : (state) => state.userData 
                                ? state.userData.userName 
                                : null,
        "ROLE_ID"             : (state) => state.userData
                                ? state.userData.roleId 
                                : null,
    },
    mutations:{
        ...defaultVuex.mutations,
        "AUTH_SUCCESS": (state) => {
            state.isAuthenticated = true;
        },
        "AUTH_ERROR": (state) => {
            state.isAuthenticated = false;
            Cookie.remove('user-token');
        },
        "AUTH_LOGOUT": (state)=>{
            state.isAuthenticated = false;
            Cookie.remove('user-token');
        },
        "SET_AUTH_INFO": (state, userData) =>{
            state.userData = {
                ...userData
            }
        },
        "CLEAR"(state){
            defaultVuex.clear(state);
            state.isAuthenticated = false;
            state.userData = null;
        }
    },
    actions:{
        ...defaultVuex.actions,
        "AUTHENTICATION" : async ({commit, dispatch}, user)=>{
            commit(defaultVuex.mutationsNames.startLoading);
            try {
                const {json} = await request(process.env.VUE_APP_AUTHENTICATION, 'POST', user);
                dispatch("LOGOUT");
                Cookie.set('user-token', json.access_token, { expires: json.timeOut * 60 });
                commit("SET_AUTH_INFO", {
                    userName: json.userName,
                    roleId : json.roleId
                })
                commit("AUTH_SUCCESS");
                commit(defaultVuex.mutationsNames.finishLoading);
            }
            catch (error) {
                if(!error.response || error.response.status !== 401){
                    commit("AUTH_ERROR");
                    commit(defaultVuex.mutationsNames.setError);
                }
                else{
                    commit("AUTH_ERROR");
                    commit(defaultVuex.mutationsNames.setError, error.response.data);
                }
                Cookie.remove('user-token');
                commit(defaultVuex.mutationsNames.finishLoading);
            }
        },
        "LOGOUT" : ({commit, dispatch}) => {
            commit("AUTH_LOGOUT");
            dispatch("CLEAR_STORE", null , {root: true})
        },
        "REGISTER" : async ({commit}, user)=>{
            commit(defaultVuex.mutationsNames.startLoading);
            try {
                await request(process.env.VUE_APP_USERS, 'POST', user);
                commit(defaultVuex.mutationsNames.finishLoading);
            }
            catch (error) {
                if(!error.response || error.response.status !== 401){
                    commit("AUTH_ERROR");
                    commit(defaultVuex.mutationsNames.setError);
                }
                else{
                    commit("AUTH_ERROR");
                    commit(defaultVuex.mutationsNames.setError, error.response.data);
                }
                commit(defaultVuex.mutationsNames.finishLoading);
            }
        },
        "LOAD_USER_DATA" : async ({commit}) => {
            commit(defaultVuex.mutationsNames.startLoading);
            try {
                const {json} = await request(process.env.VUE_APP_USERS, 'GET');
                commit("SET_AUTH_INFO", json);
                commit(defaultVuex.mutationsNames.finishLoading);
            }
            catch (error) {
                if(!error.response || error.response.status !== 400){
                    commit("AUTH_ERROR");
                    commit(defaultVuex.mutationsNames.setError);
                }
                else{
                    commit("AUTH_ERROR", error.response.data);
                    commit(defaultVuex.mutationsNames.setError, error.response.data);
                }
                commit(defaultVuex.mutationsNames.finishLoading);
            }
        }
    }
};
