import send from '../helpers/send'

export default {
    state: {
        count: null,
    },
    mutations: {
        // count
        registerCount: (state, payload) => {
            state.count = payload.count
        },
    },
    actions: {
        registerCount: async (context) => {
            const data = await send.get(context, '/api/register/count')

            context.commit('registerCount', data)
        },
        // 
        registerSend: async (context, payload) => {
            await send.post(context, '/api/register/register', payload)

            context.dispatch('registerCount')
            context.dispatch('notificationsAdd', { text: 'Registrace úspěšně odeslána', type: 'success' })
        },
    },
}
