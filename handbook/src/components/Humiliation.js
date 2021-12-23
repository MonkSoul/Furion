import React from "react";

export default function Humiliation({ name, links = [] }) {
    const _urls = links.map((url, i) => <div key={i} ><a href={url} target="_blank">{url}</a></div>)

    return <div style={{ marginBottom: 20 }}>
        <div style={{ fontWeight: 'bold' }}>{name}</div>
        <div>{_urls}</div>
    </div>
}