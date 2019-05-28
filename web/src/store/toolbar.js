export const toolbar = {
    namespaced: true,
    state:{
        drawer: false,
    },
    getters:{
        "DRAWER" : (state) => state.drawer, 
    },
    mutations:{
        "CHANGE_DRAWER": (state) => {
            state.drawer = !state.drawer;
        }
    },
    actions:{
        "CHANGE_DRAWER" :  ({commit})=>{
            commit("CHANGE_DRAWER");
        },
    }
};
