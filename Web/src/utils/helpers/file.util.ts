export function handleDownloadFile(link: string, fileName: string) {
  const a = document.createElement('a');
  a.href = link;
  a.download = fileName;

  document.body.appendChild(a);
  a.click();
  document.body.removeChild(a);
}
