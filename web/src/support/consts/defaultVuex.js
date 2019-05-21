const stateNames = {
    isLoading : 'IS_LOADING',
    error: 'ERROR'
}

const globalGettersNames = (nameSpace) => {
    return {
        isLoading : `${nameSpace}/${stateNames.isLoading}`,
        error: `${nameSpace}/${stateNames.error}`
    }
}

const mutationsNames = {
    startLoading: 'START_LOADING' ,
    finishLoading: 'FINISH_LOADING',
    setError: 'SET_ERROR',
    clear: 'CLEAR'
}

const actionsNames = {
    closeError: 'CLOSE_ERROR'
}


const actions = {
    [actionsNames.closeError]: ({commit}) => {
        commit(mutationsNames.setError, null);
    }
}
const globalActionsNames = (nameSpace) => {
    return {
        closeError : `${nameSpace}/${actions.closeError}`,
    }
}

const state = {
    [stateNames.isLoading] : false,
    [stateNames.error]: null
}
const getters = {
    [stateNames.isLoading] : (state) => state[stateNames.isLoading],
    [stateNames.error] : (state) => state[stateNames.error]
}

const mutations = {
    [mutationsNames.startLoading]: (state) => {
        state[stateNames.error] = null;
        state[stateNames.isLoading] = true;
    },
    [mutationsNames.finishLoading]: (state) => {
        state[stateNames.isLoading] = false;
    },
    [mutationsNames.setError]: (state, error = 'Что-то пошло не так. Повторите попытку позже.') => {
        state[stateNames.error] = error;
    },
}

const clear = (state) => {
    state[stateNames.isLoading] = false;
    state[stateNames.error] = null
}

export const defaultVuex = {
    stateNames,
    globalGettersNames,
    mutationsNames,
    actionsNames,
    actions,
    globalActionsNames,
    state,
    getters,
    mutations,
    clear
}