import UserOpinion from "@/components/UserOpinion";
import SarahProfilePic from "../assets/sarah.jpg";
import JohnProfilePic from "../assets/john.jpg";
import MariaProfilePic from "../assets/maria.jpg";
import TomProfilePic from "../assets/tom.jpg";

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
            <main className="mx-auto max-w-6xl px-4 sm:px-6">
                <h1 className="text-center text-5xl mt-24 md:text-6xl font-bold">
                    Rent with Confidence with Sigma&nbsp;Cars
                </h1>
                <div className="flex justify-center mt-24">
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
            </main>
            <div className="bg-slate-800 px-4 py-16 mt-24 space-y-16">
                <p className="text-justify text-white text-xl leading-relaxed max-w-6xl mx-auto">
                    At Sigma Cars, we are passionate about helping you explore the world on your terms. With our
                    extensive fleet of high-quality vehicles, you can rest assured that you will find the perfect
                    car, whether you are embarking on a cross-country adventure or need a reliable vehicle for
                    your daily commute.
                </p>
                <p className="text-neutral-400 text-base text-center">
                    Website is under development. Stay tuned for updates! Check out source code on&nbsp;
                    <a href="https://github.com/kacperwyczawski/sigma-cars" rel="noopener noreferrer"
                       className="text-blue-400 hover:underline">
                        GitHub
                    </a>
                </p>
            </div>
            <div className="mt-16 px-4">
                <h2 className="text-center text-4xl">Trusted by users all over the world!</h2>
                <div className="flex flex-row mt-8 gap-4 overflow-scroll">
                    <UserOpinion
                        name="Sarah Johnson" title="Adventure enthusiast" imageUrl={SarahProfilePic}
                        text="I rented a car from Sigma Cars for a week-long road trip, and I couldn't have been happier with the service and vehicle I received. The staff were helpful and friendly, and the car was clean and reliable. I highly recommend Sigma Cars to anyone looking for a hassle-free rental experience."/>
                    <UserOpinion
                        name="John Lee" title="Daily commuter" imageUrl={JohnProfilePic}
                        text="I've been using Sigma Cars for my daily commute for the past year, and I've never had any issues with the vehicles. They are always clean, well-maintained, and affordable. The customer service is also top-notch, making it a great overall experience."/>
                    <UserOpinion
                        name="Maria Hernandez" title="Family traveler" imageUrl={MariaProfilePic}
                        text="Sigma Cars provided us with a spacious and comfortable SUV for our family vacation. The vehicle was clean and well-equipped, making our trip stress-free and enjoyable. I would definitely use Sigma Cars again for our next family adventure."/>
                    <UserOpinion
                        name="Tom Wilson" title="Business traveler" imageUrl={TomProfilePic}
                        text="I travel for work frequently and always use Sigma Cars for my rental needs. They have a wide selection of vehicles to choose from, and the staff are always helpful and accommodating. The process is quick and easy, making it a great option for busy professionals."/>
                </div>
            </div>
        </>
    )
}
