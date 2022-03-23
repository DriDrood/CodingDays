const handleUserError = (context, message) => {
    context.dispatch('notificationsAdd', { text: message, type: 'warning' })
};
const handleSystemError = (context, message) => {
    context.dispatch('notificationsAdd', { text: message, type: 'error' })
};
const send = async (context, url, method, data = null) => {
    try {
        context.commit('setLoading', true)
        
        const headers = { 'Content-type': 'application/json' }
        if (context.state.token != null)
            headers['Authorization'] = `Bearer ${context.state.token}`

        const opt = {
            method,
            headers,
        }
        if (data != null)
            opt.body = JSON.stringify(data)
            
        const result = await fetch(url, opt)
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
                handleUserError(context, resultData)
                throw new Error(resultData)
            case 500:
                handleSystemError(context, resultData)
                throw new Error(resultData)
        }
        
        return resultData;
    }
    finally {
        context.commit('setLoading', false)
    }
};

export default {
    get: (context, url) => send(context, url, 'GET'),
    post: (context, url, data) => send(context, url, 'POST', data),
}
