import Image, {StaticImageData} from "next/image";

interface UserOpinionProps {
    name: string;
    title: string;
    text: string;
    image: StaticImageData;
}

export default function UserOpinion(props: UserOpinionProps) {
    return (
        <div className="bg-neutral-100 p-4 shadow-md shadow-neutral-100 rounded-lg w-[300px] grow shrink-0">
            <div className="flex flex-row gap-4">
                <Image
                    className="rounded-full h-12 w-12"
                    src={props.image} alt="User avatar" height={48} width={48}/>
                <div>
                    <h3 className="font-bold text-lg leading-snug">
                        {props.name}
                    </h3>
                    <h4 className="leading-snug text-neutral-500">
                        {props.title}
                    </h4>
                </div>
            </div>
            <p className="mt-4 text-lg">
                {props.text}
            </p>
        </div>
    );
}