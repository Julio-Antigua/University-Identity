export const getters = {
    isAuthentication(state){
        return state.auth.loggedIn;
    },

    loggedInUser(state){
        return state.auth.user;
    }
}