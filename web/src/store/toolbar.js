export const toolbar = {
    namespaced: true,
    state:{
        drawer: false,
    },
    getters:{
        "DRAWER" : (state) => state.drawer, 
    },
    mutations:{
        "CHANGE_DRAWER": (state, value) => {
            if(value == null){
                state.drawer = !state.drawer;
                return;
            }
            if(value == state.drawer) return;
            state.drawer = value;
        },
        "CLEAR" : () => {}
    },
    actions:{
        "CHANGE_DRAWER" :  ({commit}, value = null)=>{
            commit("CHANGE_DRAWER", value);
        },
    }
};
