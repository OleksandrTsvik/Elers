import { LocaleCode } from '../../../store/locale.slice';

import styles from './language.module.scss';

interface Props {
  language: string;
  code: LocaleCode;
}

export default function LanguageItemIcon({ language, code }: Props) {
  return (
    <span className={styles.itemIcon} aria-label={language}>
      {code}
    </span>
  );
}
