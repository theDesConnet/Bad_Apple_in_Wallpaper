@echo off
mkdir Frames
ffmpeg.exe -i BA_Original.mp4 %~f0/Frames/frame_%02d.bmp
