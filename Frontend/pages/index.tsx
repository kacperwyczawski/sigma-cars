export default function Home() {
    return (
        <main className="flex min-h-screen flex-col items-center justify-between p-24">
            <div className="z-10 w-full max-w-5xl items-center justify-between text-sm lg:flex">
                <p className="text-lg fixed left-0 top-0 flex w-full justify-center border-b border-gray-300 bg-gradient-to-b from-zinc-200 pb-6 pt-8 backdrop-blur-2xl dark:border-neutral-800 dark:bg-zinc-800/30 dark:from-inherit lg:static lg:w-auto  lg:rounded-xl lg:border lg:bg-gray-200 lg:p-4 lg:dark:bg-zinc-800/30">
                    Sigma Cars is under development
                </p>
            </div>

            <div
                className="relative flex place-items-center before:absolute before:h-[300px] before:w-[480px] before:-translate-x-1/2 before:rounded-full before:bg-gradient-radial before:from-white before:to-transparent before:blur-2xl before:content-[''] after:absolute after:-z-20 after:h-[180px] after:w-[240px] after:translate-x-1/3 after:bg-gradient-conic after:from-sky-200 after:via-blue-200 after:blur-2xl after:content-[''] before:dark:bg-gradient-to-br before:dark:from-transparent before:dark:to-blue-700/10 after:dark:from-sky-900 after:dark:via-[#0141ff]/40 before:lg:h-[360px]">
                <p className="text-6xl font-bold text-glow">
                    &Sigma;&nbsp;Cars
                </p>
            </div>

            <footer className="font-mono">
                Stay tuned for updates! Check out source code on&nbsp;
                <a href="https://github.com/kacperwyczawski/sigma-cars" rel="noopener noreferrer"
                   className="text-blue-500 hover:underline">
                    GitHub
                </a>
            </footer>
        </main>
    )
}
