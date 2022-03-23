import send from '../helpers/send'

export default {
    state: {
        token: null,
    },
    mutations: {
        // token
        login: (state, payload) => {
            state.token = payload.token
        },
    },
    actions: {
        // name, password
        login: async (context, payload) => {
            const data = await send.post(context, '/api/team/login', payload)

            context.commit('login', data)
            context.dispatch('notificationsAdd', { text: 'Úspěšně přihlášen', type: 'success' })
        },
    },
}