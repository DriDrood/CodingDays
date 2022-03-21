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
            const resultBody = await result.text()
            let resultData = null;
            try
            {
                resultData = JSON.parse(resultBody)
            }
            catch
            {
                resultData = resultBody
            }

            switch (result.status) {
                case 400:
                    privateMethods.handleUserError(context, resultData)
                    throw new Error(resultData)
                case 500:
                    privateMethods.handleSystemError(context, resultData)
                    throw new Error(resultData)
            }
            
            return resultData;
        }
        finally {
            context.commit('setLoading', false)
        }
    },
}
