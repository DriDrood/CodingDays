import { v4 } from 'uuid'

export default {
    state: {
        loading: false,
        items: { },
    },
    mutations: {
        // true|false
        setLoading: (state, payload) => {
            state.loading = payload
        },
        // id, text, type
        notificationsAdd: (state, payload) => {
            state.items[payload.id] = { text: payload.text, type: payload.type }
        },
        // id
        notificationsRemove: (state, payload) => {
            delete state.items[payload.id]
        }
    },
    actions: {
        // text, type
        notificationsAdd: (context, payload) => {
            const id = v4()
            context.commit('notificationsAdd', { id, ...payload })

            setTimeout(() => context.commit('notificationsRemove', { id }), 10000)
        }
    }
}