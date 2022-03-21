import send from "../helpers/send"

export default {
    state: {
        teams: { },
    },
    mutations: {
        // teams
        homeLoadTeams: (state, payload) => {
            state.teams = payload.teams
        },
    },
    actions: {
        homeLoadTeams: async (context) => {
            const data = await send.get(context, '/api/team/get')

            context.commit('homeLoadTeams', data)
        },
    },
}