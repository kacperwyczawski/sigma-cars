export default function Home() {
    return (
        <>
            <header className="flex bg-orange-600 p-2 sm:p-4 justify-between items-center text-white">
                <div className="flex gap-2 text-xl sm:text-2xl font-bold">
                    <div className="bg-slate-800 w-8 h-8 grid place-items-center rounded-md">
                        &Sigma;
                    </div>
                    <div className="leading-8">
                        Sigma Cars
                    </div>
                </div>
                <div className="flex-row font-normal text-lg sm:text-xl divide-x-2">
                    <a href="/" className="px-2">
                        Log in
                    </a>
                    <a href="/" className="px-2">
                        Sign up
                    </a>
                </div>
            </header>
            <main className="mx-auto max-w-4xl mt-40 p-3">
                <h1 className="text-center text-5xl md:text-6xl font-semibold">
                    Rent with Confidence with Sigma&nbsp;Cars
                </h1>
                <div className="flex justify-center mt-16">
                    <div className="flex flex-row">
                        <div className="basis-2 flex flex-col gap-2">
                            <label
                                className="ml-4 text-lg"
                                htmlFor="from">
                                From:
                            </label>
                            <input
                                className="bg-transparent block p-4 rounded-l-lg border"
                                type="text" id="from"/>
                        </div>
                        <div className="basis-2 flex flex-col gap-2">
                            <label
                                className="ml-4 text-lg"
                                htmlFor="to">
                                To:
                            </label>
                            <input
                                className="bg-transparent block p-4 border-y"
                                type="text" id="to"/>
                        </div>
                        <div className="basis-1 flex items-end">
                            <input
                                className="bg-slate-800 hover:bg-slate-700 block p-4 rounded-r-lg border-slate-800 hover:border-slate-700 border text-white"
                                type="submit" value="Explore!"/>
                        </div>
                    </div>
                </div>
                <h2 className="text-center mt-16 text-xl text-neutral-800">
                    <p className="text-justify">
                        At Sigma Cars, we are passionate about helping you explore the world on your terms. With our
                        extensive fleet of high-quality vehicles, you can rest assured that you will find the perfect
                        car, whether you are embarking on a cross-country adventure or need a reliable vehicle for
                        your daily commute.
                    </p>
                    <p className="mt-10 text-neutral-400 text-base">
                        Website is under development. Stay tuned for updates! Check out source code on&nbsp;
                        <a href="https://github.com/kacperwyczawski/sigma-cars" rel="noopener noreferrer"
                           className="text-blue-400 hover:underline">
                            GitHub
                        </a>
                    </p>
                </h2>
            </main>
        </>
    )
}
