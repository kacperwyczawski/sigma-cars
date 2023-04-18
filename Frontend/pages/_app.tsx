import '@/styles/globals.css'
import type {AppProps} from 'next/app'
import {Fira_Code} from "next/font/google";
import {Inter} from 'next/font/google'

const firaCode = Fira_Code({
    subsets: ['latin'],
    variable: '--font-fira-code'
})

const inter = Inter({
    subsets: ['latin'],
    variable: '--font-inter'
})

export default function App({Component, pageProps}: AppProps) {
    return (
        <div className={`${inter.variable} font-sans ${firaCode.variable} font-mono`}>
            <Component {...pageProps} />
        </div>
    )
}
