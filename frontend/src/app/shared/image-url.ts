const apiUrl = 'http://localhost:5124';

export function imageUrl(lien: string): string {
  if (/^(https?:|data:|blob:)/i.test(lien)) {
    return lien;
  }

  if (lien.startsWith('/images/')) {
    return `${apiUrl}${lien}`;
  }

  return lien;
}
