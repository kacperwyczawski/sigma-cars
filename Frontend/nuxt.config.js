// noinspection SpellCheckingInspection

export default defineNuxtConfig({
    ssr: false,
    modules: ["@nuxtjs/tailwindcss"],
    app: {
        pageTransition: {
            name: "page",
            mode: "out-in"
        },
        head: {
            title: "Sigma Cars",
            link: [
                {
                    rel: "preconnect",
                    href: "https://fonts.googleapis.com"
                },
                {
                    rel: "preconnect",
                    href: "https://fonts.gstatic.com",
                    crossorigin: ""
                },
                {
                    rel: "stylesheet",
                    href: "https://fonts.googleapis.com/css2?family=Fira+Code&family=Inter:wght@400;600;700&display=swap"
                },
                {
                    rel: "favicon",
                    href: "/favicon.svg"
                }
            ]
        }
    }
});
