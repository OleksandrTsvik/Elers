import { LocaleCode } from '../../../store/locale.slice';

interface Props {
  language: string;
  code: LocaleCode;
}

export default function LanguageItemIcon({ language, code }: Props) {
  return (
    <span aria-label={language} style={{ marginRight: 8 }}>
      {code}
    </span>
  );
}
