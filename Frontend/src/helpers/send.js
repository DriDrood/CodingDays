const privateMethods = {
    handleUserError: (context, message) => {
        context.dispatch('notificationsAdd', { text: message, type: 'warning' })
    },
    handleSystemError: (context, message) => {
        context.dispatch('notificationsAdd', { text: message, type: 'error' })
    },
};

export default {
    get: async (context, url) => {
        try {
            context.commit('setLoading', true)

            const result = await fetch(url)
            return await result.json()
        }
        finally {
            context.commit('setLoading', false)
        }
    },
    post: async (context, url, data) => {
        try {
            context.commit('setLoading', true)
            const result = await fetch(url, { method: 'POST', headers: { 'Content-type': 'application/json' }, body: JSON.stringify(data) })
            const resultData = await result.json()

            switch (result.status) {
                case 400:
                    privateMethods.handleUserError(context, resultData.message)
                    throw new Error(resultData.message)
                case 500:
                    privateMethods.handleSystemError(context, resultData.message)
                    throw new Error(resultData.message)
            }
            
            return resultData;
        }
        finally {
            context.commit('setLoading', false)
        }
    },
}
