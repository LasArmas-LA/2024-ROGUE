# GFF-2024-ROUGE

制作におけるチーム全員が効率よく作業ができるよう情報をまとめてあります。  
最新情報は随時追加予定  
ホームページにしてあるため知りたい情報があるときは見てみましょう。

アルファ版完成予定日は
# 12月31日 23時59分

## データ関連

### [Google ドライブ](https://drive.google.com/drive/folders/1qNsUP2GD4svIqNEIUb-bsCz-2AeszBqh?usp=drive_link)

### [仕様書-ExcelWeb](https://1drv.ms/x/c/1d234c969815360f/ET0u2ed3cJtCog1Kp05bh5wBwg3yCm4njS2yuXTas_RA5Q?e=oSuMdX)  
※ExcelのWeb版でしか編集ができませんが無料でアプリを持っていなくても可能でございます。
　仕様書にはゲームのパラメーターや細かい指示が記入されています。

### [企画書](https://1drv.ms/p/c/1d234c969815360f/ESqPrr3c9TlEqiBNBiM6A5YBrcWuF49uA7VqEQeZ6kJBag?e=Xxi6W6)

### [イラストドライブ](https://drive.google.com/drive/folders/1UdTZgEOx7ecX6RMIFruUXqO9_YkQYcYf?usp=drive_link)

### [ドキュメント](https://drive.google.com/drive/folders/1k5T3OlEwNeazykKaJMlata8nsvBa4Ml3?usp=drive_link)

### [シナリオドライブ](https://drive.google.com/drive/folders/1lVZxuk0Klrc_xUi2M3J4TosaI_DBLwsS?usp=drive_link)

## 技術要素

| 開発環境     | バージョン       | 詳細                   |
| ------------ | ---------------- | ---------------------- |
| VisualStudio | 2024.17.12.0     | プログラムコードの入力 |
| Unity        | 6000.0.27f1(LTS) | ゲームエンジン         |
| Windows      | 11               | Windows 環境           |
| GitHub       |                  | データのリモート共有   |

## イラスト

下記はイラスト共有時に守ってもらいたいルールです。
- 提出はグーグルドライブで提出してください。  
（サイズが大きいときはギガファイルを使用しましょう）
- "PNG"データで共有をしてください。
- カラーモードは"RGB"で作成してください。
- ラスタライズ効果は"PPI"にしてください。
- 解像度は"300"PPIとしてください。
- サイズ（比率）はプランナーに聞いてください。
- サイズ（幅と高さ）は"ピクセル"で作成してください。

## ファイルの命名規則について
タイトル、文章、の最初の文字は大文字にすること
※英語、ローマ字記入の場合

```
悪い例
riri.png
riri_haikei.png

良い例
Riri.png
Riri_Haikei.png
```


ドキュメント、スプレッドシート、スライドを除くデータ名は基本英語又はローマ字を使用すること。  

```
悪い例
リリー.png

良い例
Riri.png
```

スペース（インデント）を記入するときはアンダーバーを使用すること
```
悪い例
Rriri Haikei.png
Rriri-Haikei.png

よい例
Riri_Haikei.png

```

## 制作 Topics

- 質問をする  
  自己解決をするとチームとの認識合わせをしない事になる
  解らない単語・文章・概念は遠慮なく質問しよう！

- 仕様書をよく見よう  
  考えの食い違いがあると後々大変です。

- 概念を理解しよう  
  詳細よりも大枠を理解するだけでも全然違います。

- 解決よりも現状を整理しよう！
- Why?(なぜ？)を考えよう！
- 提出が間に合わないときは早めに相談を！！
- 意見があるときは共有しよう！


## Git コミットメッセージ

GitHub の Commit（コミット）メッセージの意味は下記を参照

| 命名規則 | 意味                       |
| -------- | -------------------------- |
| fix      | バグ修正                   |
| add      | 既存ファイルや機能の追加   |
| update   | 機能修正                   |
| change   | 仕様による機能修正         |
| clean    | コードの改善、改善         |
| disable  | 機能を一時的に無効         |
| remove   | ファイルや、機能を削除     |
| rename   | ファイル名を変更           |
| move     | ファイルを移動             |
| upgrade  | バージョンアップ           |
| revert   | 以前のコミットに戻す<br>   |
| docs     | ドキュメントを修正<br>     |
| test     | テストコードの追加や修正   |
| chore    | その他                     |
| style    | コーディングスタイルの修正 |
| perf     | パフォーマンスを改善       |

## 拡張機能

- Discord に GitHub 連携 BOT の実装  
  ┗ プッシュ時のメッセージを送信

### Created by LasArmas
