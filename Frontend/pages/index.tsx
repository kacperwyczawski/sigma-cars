import UserOpinion from "@/components/UserOpinion";
import SarahProfilePic from "../assets/sarah.jpg";
import JohnProfilePic from "../assets/john.jpg";
import MariaProfilePic from "../assets/maria.jpg";
import TomProfilePic from "../assets/tom.jpg";
import {Listbox, Transition} from "@headlessui/react";
import React, {Fragment, useState} from "react";
import {Calendar, Check, ChevronsUpDown} from "lucide-react";


// fake data
const departments = [
    "Chicago",
    "Boston",
    "Kansas",
];
const userOpinions = [
    {
        name: "Sarah Johnson",
        title: "Adventure enthusiast",
        image: SarahProfilePic,
        text: "Sigma Cars made my road trip unforgettable. The car was reliable and the service was exceptional. Highly recommended for adventure seekers like me!"
    },
    {
        name: "John Lee",
        title: "Daily commuter",
        image: JohnProfilePic,
        text: "I rely on Sigma Cars for my daily commute and they never disappoint. Clean and well-maintained cars, affordable prices, and great customer service. What else can I ask for?"
    },
    {
        name: "Maria Hernandez",
        title: "Family traveler",
        image: MariaProfilePic,
        text: "Thanks to Sigma Cars, my family and I had a wonderful vacation. The SUV was spacious and comfortable, and the staff were friendly and helpful. Can't wait for our next adventure!"
    },
    {
        name: "Tom Wilson",
        title: "Business traveler",
        image: TomProfilePic,
        text: "Sigma Cars is my go-to for work travel. With a wide selection of vehicles and a hassle-free rental process, they make my trips smoother and more enjoyable. Highly recommended for busy professionals!"
    }
];

const defaultStartDate = new Date()
const defaultEndDate = new Date()
defaultEndDate.setDate(defaultEndDate.getDate() + 7)

export default function Home() {
    const [department, setDepartment] = useState(departments[0]);
    const [startDate, setStartDate] = useState(defaultStartDate.toISOString().slice(0, 10));
    const [endDate, setEndDate] = useState(defaultEndDate.toISOString().slice(0, 10));

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
                    <div className="max-w-3xl flex gap-3 items-end flex-grow flex-wrap">
                        <label className="flex-grow relative">
                            From:
                            <input
                                className="block border w-full py-2 px-3 rounded-md h-10 mt-2"
                                type="date" value={startDate}
                                onChange={event => setStartDate(event.target.value)}
                                max="01-01-2200"
                                min="01-01-2000"/>
                            <span className={"pointer-events-none absolute right-4 bottom-2.5 bg-white"}>
                                <Calendar className={"text-slate-800 h-5 w-5"}/>
                            </span>
                        </label>
                        <label className="flex-grow relative">
                            To:
                            <input
                                className="block border w-full py-2 px-3 rounded-md h-10 mt-2"
                                type="date" value={endDate}
                                onChange={event => setEndDate(event.target.value)}
                                max="01-01-2200"
                                min="01-01-2000"/>
                            <span className={"pointer-events-none absolute right-4 bottom-2.5 bg-white"}>
                                <Calendar className={"text-slate-800 h-5 w-5"}/>
                            </span>
                        </label>
                        <label className="flex-grow">
                            Location:
                            <div className="w-full">
                                <Listbox
                                    value={department} onChange={setDepartment}>
                                    <div className="relative mt-1">
                                        <Listbox.Button
                                            className="block border w-full py-2 px-3 rounded-md h-10 mt-2 pr-16 bg-white text-left">
                                            <span className="block truncate">{department}</span>
                                            <span
                                                className="pointer-events-none absolute inset-y-0 right-0 flex items-center pr-2">
                                    <ChevronsUpDown
                                        className="h-5 w-5 text-slate-800"
                                        aria-hidden="true"/>
                                    </span>
                                        </Listbox.Button>
                                        <Transition
                                            as={Fragment}
                                            leave="transition ease-in duration-100"
                                            leaveFrom="opacity-100"
                                            leaveTo="opacity-0">
                                            <Listbox.Options
                                                className="absolute mt-1 max-h-60 w-full overflow-auto rounded-md bg-white py-1 text-base shadow-lg ring-1 ring-black ring-opacity-5 focus:outline-none sm:text-sm">
                                                {departments.map((department) => (
                                                    <Listbox.Option
                                                        className="relative cursor-default select-none py-2 pl-10 pr-4 hover:bg-slate-100"
                                                        key={department}
                                                        value={department}>
                                                        {({selected}) => selected ? (
                                                            <>
                                                                <div className={"block truncate font-bold"}>
                                                                    {department}
                                                                </div>
                                                                <span
                                                                    className="absolute inset-y-0 left-0 flex items-center pl-3 text-slate-800">
                                                                    <Check className="h-5 w-5" aria-hidden="true"/>
                                                                </span>
                                                            </>
                                                        ) : (
                                                            <div className={"block truncate"}>
                                                                {department}
                                                            </div>
                                                        )}
                                                    </Listbox.Option>
                                                ))}
                                            </Listbox.Options>
                                        </Transition>
                                    </div>
                                </Listbox>
                            </div>
                        </label>
                        <input
                            className="block border py-2 px-3 rounded-md h-10 flex-grow bg-slate-800 text-white border-none hover:bg-slate-700"
                            type="submit" value="Explore!"/>
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
                    {userOpinions.map((opinion, index) => (
                        <UserOpinion key={opinion.name} {...opinion}/>
                    ))}
                </div>
            </div>
        </>
    )
}
